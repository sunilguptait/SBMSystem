import { Component, OnInit } from '@angular/core';
import { AuthenticationService, SessionService } from '../../../../services';
import { Router } from '@angular/router';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-admin-header',
  templateUrl: './admin-header.component.html',
  styleUrls: ['./admin-header.component.scss']
})
export class AdminHeaderComponent implements OnInit {
  currentUser: any = {}
  pageTitle: string;
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private broadcasterService: BroadCasterService,
    private sessionService: SessionService
  ) { }

  ngOnInit() {
    this.currentUser = this.sessionService.getLoggedInUser()
    this.broadcasterService.on('PageTitle').subscribe(m => {
      this.pageTitle = m;
    })
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(["/login"]);
  }

}
