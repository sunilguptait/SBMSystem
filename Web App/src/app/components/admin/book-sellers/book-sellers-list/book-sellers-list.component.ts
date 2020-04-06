import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService, SessionService } from '../../../../services';
import { BookSellerService } from '../../../../services/book-seller.service';
import { MatSort, MatPaginator } from '@angular/material';
import { SessionKeys } from '../../../../common/session-keys';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-book-sellers-list',
  templateUrl: './book-sellers-list.component.html',
  styleUrls: ['./book-sellers-list.component.scss']
})
export class BookSellersListComponent implements OnInit {

  displayedColumns: string[] = ['SNo', 'RegistrationNo', 'FirmName', 'FirstName', 'MobileNo', 'EmailId', 'Address1', 'actions'];
  bookSellersList: any = [];
  pageSize = 10;
  pageIndex = 0;
  totalSize = 0;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    private router: Router,
    private sessionKeys: SessionKeys,
    private commonService: CommonService,
    private sessionService: SessionService,
    private bookSellerService: BookSellerService,
    private broadcasterService: BroadCasterService,
  ) { }

  ngOnInit() {
    this.sort.sortChange.subscribe(() => {
      // this.paginator.pageIndex = 0;
      this.pageIndex = 0;
      this.getBookSellersList();
    });
    this.broadcasterService.broadcast("PageTitle", "Book Seller's");
    this.getBookSellersList();
  }

  getBookSellersList() {
    this.bookSellersList = []
    var searchParams = {
      PageSize: this.pageSize,
      PageIndex: this.pageIndex + 1,
      SortBy: this.sort.active || 'FirstName',
      SortDirection: this.sort.direction || 'asc'
    }
    this.commonService.showSpinner();
    this.bookSellerService.getList(searchParams).subscribe(response => {
      console.log(response);
      if (!this.commonService.validateAPIResponse(response)) {
        return; // show error message and return in case of any error from API
      }
      this.bookSellersList = response.Data;
      this.totalSize = response.TotalItems;

    })
  }

  handlePage(e: any) {
    console.log('on page change')
    this.pageIndex = e.pageIndex;
    this.pageSize = e.pageSize;
    this.getBookSellersList();
  }

  onEditClick(element) {
    this.sessionService.setSession(this.sessionKeys.Keys.BookSeller.Details, element);
    debugger;
    let url = `/admin/edit-book-seller/${element.Id}`;
    this.router.navigateByUrl(`/admin/edit-book-seller/${element.Id}`);
  }
}
