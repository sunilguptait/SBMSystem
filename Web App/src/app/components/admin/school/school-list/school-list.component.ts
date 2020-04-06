import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService, SessionService } from '../../../../services';
import { BookSellerService } from '../../../../services/book-seller.service';
import { MatSort, MatPaginator } from '@angular/material';
import { SessionKeys } from '../../../../common/session-keys';
import { SchoolService } from '../../../../services/school.service';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-school-list',
  templateUrl: './school-list.component.html',
  styleUrls: ['./school-list.component.scss']
})
export class SchoolListComponent implements OnInit {
  displayedColumns: string[] = ['SNo','Name','Address','MobileNo', 'actions'];
  schoolList: any = [];
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
    private schoolService: SchoolService,
    private broadcasterService: BroadCasterService,
  ) { }

  ngOnInit() {
    this.broadcasterService.broadcast("PageTitle", "School's");
    this.sort.sortChange.subscribe(() => {
      // this.paginator.pageIndex = 0;
      this.pageIndex = 0;
      this.getSchoolsList();
    });

    this.getSchoolsList();
  }

  getSchoolsList() {
    this.schoolList = []
    var searchParams = {
      PageSize: this.pageSize,
      PageIndex: this.pageIndex + 1,
      SortBy: this.sort.active || 'Name',
      SortDirection: this.sort.direction || 'asc'
    }
    this.commonService.showSpinner();
    this.schoolService.getList(searchParams).subscribe(response => {
      console.log(response);
      if (!this.commonService.validateAPIResponse(response)) {
        return; // show error message and return in case of any error from API
      }
      this.schoolList = response.Data;
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
    this.sessionService.setSession(this.sessionKeys.Keys.School.Details, element);
    this.router.navigateByUrl(`/admin/school/edit/${element.Id}`);
  }

  onDeleteClick(element) {
    debugger;
    this.commonService.showSpinner();
    this.schoolService.delete(element.Id).subscribe(response => {
      if (!this.commonService.validateAPIResponse(response)) {
        return; // show error message and return in case of any error from API
      }
      this.commonService.showSuccessMessage(response.Data);
      this.getSchoolsList();
    })
  }

}
