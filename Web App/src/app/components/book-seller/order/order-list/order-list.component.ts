import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatPaginator } from '@angular/material';
import { Router } from '@angular/router';
import { SessionKeys } from 'src/app/common/session-keys';
import { CommonService, SessionService } from 'src/app/services';
import { OrderService } from 'src/app/services/order.service';
import { SelectionModel } from '@angular/cdk/collections';
import { BroadCasterService } from 'src/app/services/broad-caster.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit {
  displayedColumns: string[] = ['Select', 'Order_Code', 'St_Name', 'Order_Date', 'Order_PaymentMode', 'Order_PaymentStatus', 'Order_Status', 'Order_TotalAmount', 'actions'];
  orderList: any = [];
  pageSize = 10;
  pageIndex = 0;
  totalSize = 0;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  selection = new SelectionModel<any>(true, []);

  constructor(private router: Router,
    private sessionKeys: SessionKeys,
    private commonService: CommonService,
    private sessionService: SessionService,
    private orderService: OrderService,
    private broadcasterService: BroadCasterService
  ) { }

  ngOnInit() {
    this.broadcasterService.broadcast("PageTitle", "Orders");
    this.sort.sortChange.subscribe(() => {
      this.pageIndex = 0;
      this.getOrdersList();
    });

    this.getOrdersList();
  }

  getOrdersList() {
    this.orderList = []
    var searchParams = {
      PageSize: this.pageSize,
      PageIndex: this.pageIndex + 1,
      SortBy: this.sort.active,
      SortDirection: this.sort.direction
    }
    this.commonService.showSpinner();
    this.orderService.getOrders(searchParams).subscribe(response => {
      if (!this.commonService.validateAPIResponse(response)) {
        return;
      }
      this.orderList = response.Data;
      this.totalSize = response.TotalItems;

    })
  }

  handlePage(e: any) {
    this.pageIndex = e.pageIndex;
    this.pageSize = e.pageSize;
    this.getOrdersList();
  }

  showInvoice(order) {
    this.getInvoices([order.Order_Code]);
  }

  getInvoices(orderCodes) {
    this.commonService.showSpinner();
    this.orderService.getInvoices({ Orders: orderCodes }).subscribe(m => {
      this.commonService.downloadBlobFile(m, "PDF");
      this.commonService.hideSpinner();
    });
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.orderList.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.orderList.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  printInvoice() {
    let selectedOrderCodes = this.selection.selected.map(m => m.Order_Code);
    if (selectedOrderCodes && selectedOrderCodes.length > 0) {
      this.getInvoices(selectedOrderCodes);
    }
    else {
      this.commonService.showErrorMessage('Please select at least one order.');
    }
  }
}
