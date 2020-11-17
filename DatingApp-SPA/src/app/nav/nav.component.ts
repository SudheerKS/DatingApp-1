import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { async } from '@angular/core/testing';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

   model: any = {};
   loggedin: boolean;
   currentUser$: Observable<User>;


  constructor(public accountService: AccountService) { }

  ngOnInit() {
    this.currentUser$ = this.accountService.currentUser$;
    // console.log(this.accountService.currentUser$);
  }


  login() {
      this.accountService.login(this.model).subscribe(response => {
      this.loggedin = true;
     }, error => {
      console.log(error);
    });
   }

   loggedOut() {
     this.accountService.logout();
     this.loggedin = false;
   }
  }


