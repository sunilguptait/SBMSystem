import { Component, OnInit } from '@angular/core';
import { AuthenticationService, SessionService, CommonService } from '../../../../services';
import { Router } from '@angular/router';
import { BroadCasterService } from 'src/app/services/broad-caster.service';

@Component({
  selector: 'app-book-seller-header',
  templateUrl: './book-seller-header.component.html',
  styleUrls: ['./book-seller-header.component.scss']
})
export class BookSellerHeaderComponent implements OnInit {
  currentUser: any = {}
  pageTitle: string;
  selectedSchool: any;
  selectionSchools: any[];
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private sessionService: SessionService,
    private broadcasterService: BroadCasterService,
    private commonService: CommonService
  ) { }

  ngOnInit() {
    this.currentUser = this.sessionService.getLoggedInUser()
    this.broadcasterService.on('PageTitle').subscribe(m => {
      this.pageTitle = m;
    })
    this.getSelectedSchool();
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(["/login"]);
  }

  getSelectedSchool() {
    this.selectionSchools = this.currentUser.Schools ? this.currentUser.Schools.filter(m => m.isSelected != true) : [];
    this.selectedSchool = this.commonService.getSelectedSchool();
  }

  selectSchool(schoolId) {
    this.currentUser = this.sessionService.getLoggedInUser();
    if (this.currentUser.Schools && this.currentUser.Schools.length > 0) {
      this.currentUser.Schools.forEach(element => {
        if (element.Id == schoolId) {
          element.isSelected = true;
        }
        else {
          element.isSelected = false;
        }
      });
      this.commonService.updateCurrentUser(this.currentUser);
      this.getSelectedSchool();
    }
  }
}
