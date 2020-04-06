import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService, SessionService } from '../../../../services';
import { BookSellerService } from '../../../../services/book-seller.service';
import { MatSort, MatPaginator } from '@angular/material';
import { SessionKeys } from '../../../../common/session-keys';
import { ClassService } from '../../../../services/class.service';
import { PublisherService } from '../../../../services/publisher.service';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-publisher-list',
  templateUrl: './publisher-list.component.html',
  styleUrls: ['./publisher-list.component.scss']
})
export class PublisherListComponent implements OnInit {

  displayedColumns: string[] = ['SNo','Name','Address','MobileNo', 'actions'];
  publisherList: any = [];
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
    private publisherService: PublisherService,
    private broadcasterService: BroadCasterService,
  ) { }

  ngOnInit() {
    this.broadcasterService.broadcast("PageTitle", " Publisher's");
    this.sort.sortChange.subscribe(() => {
      // this.paginator.pageIndex = 0;
      this.pageIndex = 0;
      this.getSchoolsList();
    });

    this.getSchoolsList();
  }

  getSchoolsList() {
    this.publisherList = []
    var searchParams = {
      PageSize: this.pageSize,
      PageIndex: this.pageIndex + 1,
      SortBy: this.sort.active || 'Name',
      SortDirection: this.sort.direction || 'asc'
    }
    this.commonService.showSpinner();
    this.publisherService.getList(searchParams).subscribe(response => {
      console.log(response);
      if (!this.commonService.validateAPIResponse(response)) {
        return; // show error message and return in case of any error from API
      }
      this.publisherList = response.Data;
      this.totalSize = response.TotalItems;

    })
  }

  handlePage(e: any) {
    console.log('on page change')
    this.pageIndex = e.pageIndex;
    this.pageSize = e.pageSize;
    this.getSchoolsList();
  }

  onEditClick(element) {
    this.sessionService.setSession(this.sessionKeys.Keys.Publisher.Details, element);
    this.router.navigateByUrl(`/book-seller/publisher/edit/${element.Id}`);
  }
}
