import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService, SessionService } from '../../../../services';
import { BookSellerService } from '../../../../services/book-seller.service';
import { MatSort, MatPaginator } from '@angular/material';
import { SessionKeys } from '../../../../common/session-keys';
import { BookService } from '../../../../services/book.service';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {
  displayedColumns: string[] = ['SNo','Book_Name','Book_ShortName','PublisherName','BookType', 'actions'];
  bookList: any = [];
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
    this.broadcasterService.broadcast("PageTitle", "Book's");
    this.sort.sortChange.subscribe(() => {
      // this.paginator.pageIndex = 0;
      this.pageIndex = 0;
      this.getBooksList();
    });

    this.getBooksList();
  }

  getBooksList() {
    this.bookList = []
    var searchParams = {
      PageSize: this.pageSize,
      PageIndex: this.pageIndex + 1,
      SortBy: this.sort.active || 'Book_Name',
      SortDirection: this.sort.direction || 'asc'
    }
    this.commonService.showSpinner();
    this.bookService.getList(searchParams).subscribe(response => {
      console.log(response);
      if (!this.commonService.validateAPIResponse(response)) {
        return; // show error message and return in case of any error from API
      }
      this.bookList = response.Data;
      this.totalSize = response.TotalItems;

    })
  }

  handlePage(e: any) {
    console.log('on page change')
    this.pageIndex = e.pageIndex;
    this.pageSize = e.pageSize;
    this.getBooksList();
  }

  onEditClick(element) {
    this.sessionService.setSession(this.sessionKeys.Keys.Book.Details, element);
    this.router.navigateByUrl(`/book-seller/book/edit/${element.Id}`);
  }
}
