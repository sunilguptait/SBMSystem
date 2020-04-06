import { Component, OnInit } from '@angular/core';
import { BroadCasterService } from '../../../services/broad-caster.service';

@Component({
  selector: 'app-book-seller-dashboard',
  templateUrl: './book-seller-dashboard.component.html',
  styleUrls: ['./book-seller-dashboard.component.scss']
})
export class BookSellerDashboardComponent implements OnInit {

  constructor(
    private broadcasterService: BroadCasterService,
  ) { }

  ngOnInit() {
    this.broadcasterService.broadcast("PageTitle", "Dashboard");
  }

}
