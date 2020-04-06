import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService, SessionService } from '../../../../services';
import { BookSellerService } from '../../../../services/book-seller.service';
import { MatSort, MatPaginator } from '@angular/material';
import { SessionKeys } from '../../../../common/session-keys';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-book-seller-school-mapping-list',
  templateUrl: './book-seller-school-mapping-list.component.html',
  styleUrls: ['./book-seller-school-mapping-list.component.scss']
})
export class BookSellerSchoolMappingListComponent implements OnInit {

  displayedColumns: string[] = ['SNo','BookSellerName', 'SchoolName', 'actions'];
  mappingList: any = [];
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
      this.getDataList();
    });
    this.broadcasterService.broadcast("PageTitle", " Book Seller School Mapping");
    this.getDataList();
  }

  getDataList() {
    this.mappingList = []
    var searchParams = {
      PageSize: this.pageSize,
      PageIndex: this.pageIndex + 1,
      SortBy: this.sort.active || 'BookSellerName',
      SortDirection: this.sort.direction || 'asc'
    }
    this.commonService.showSpinner();
    this.bookSellerService.getBookSellerSchoolMappingList(searchParams).subscribe(response => {
      console.log(response);
      if (!this.commonService.validateAPIResponse(response)) {
        return; // show error message and return in case of any error from API
      }
      this.mappingList = response.Data;
      this.totalSize = response.TotalItems;

    })
  }

  handlePage(e: any) {
    console.log('on page change')
    this.pageIndex = e.pageIndex;
    this.pageSize = e.pageSize;
    this.getDataList();
  }

  onDeleteClick(element) {
    // this.sessionService.setSession(this.sessionKeys.Keys.Class.Details, element);
    // this.router.navigateByUrl(`/admin/class/edit/${element.Id}`);

    this.commonService.showSpinner();
    this.bookSellerService.deleteBookSellerSchool(element.SSM_Id).subscribe(response => {
      if (!this.commonService.validateAPIResponse(response)) {
        return; // show error message and return in case of any error from API
      }
      this.commonService.showSuccessMessage(response.Data);
      this.getDataList();
    })
  }
}