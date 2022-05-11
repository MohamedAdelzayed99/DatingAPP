import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account---skip-tests.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any={}
  LoggedIn: boolean;
  constructor(public accountservice: AccountService) { }

  ngOnInit(): void {

  }

  Login()
  {
    this.accountservice.Login(this.model).subscribe(response => {
      console.log(response);

    },error =>{
      console.log(error);
    })
  }

  Logout()
  {
    this.accountservice.Logout();
  }

}
