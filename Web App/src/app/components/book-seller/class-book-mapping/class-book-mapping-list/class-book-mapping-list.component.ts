import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService, SessionService } from '../../../../services';
import { BookSellerService } from '../../../../services/book-seller.service';
import { MatSort, MatPaginator } from '@angular/material';
import { SessionKeys } from '../../../../common/session-keys';
import { BookService } from '../../../../services/book.service';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-class-book-mapping-list',
  templateUrl: './class-book-mapping-list.component.html',
  styleUrls: ['./class-book-mapping-list.component.scss']
})
export class ClassBookMappingListComponent implements OnInit {

  displayedColumns: string[] = ['SNo','BookName', 'ClassName','BCM_DefaultQty', 'actions'];
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
    private bookService: BookService,
    private broadcasterService: BroadCasterService,
  ) { }

  ngOnInit() {
    this.broadcasterService.broadcast("PageTitle", "Class Book Mapping");
    this.sort.sortChange.subscribe(() => {
      // this.paginator.pageIndex = 0;
      this.pageIndex = 0;
      this.getDataList();
    });

    this.getDataList();
  }

  getDataList() {
    this.mappingList = []
    var searchParams = {
      PageSize: this.pageSize,
      PageIndex: this.pageIndex + 1,
      SortBy: this.sort.active || 'BookName',
      SortDirection: this.sort.direction || 'asc'
    }
    this.commonService.showSpinner();
    this.bookService.getBookClassMappingList(searchParams).subscribe(response => {
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
    this.commonService.showSpinner();
    this.bookService.deleteBookClassMapping(element.BCM_Id).subscribe(response => {
      if (!this.commonService.validateAPIResponse(response)) {
        return; // show error message and return in case of any error from API
      }
      this.commonService.showSuccessMessage(response.Data);
      this.getDataList();
    })
  }
}