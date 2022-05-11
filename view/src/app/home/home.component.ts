import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  users: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
//this.getUser();
  }

  registerToggel()
  {
    this.registerMode= !this.registerMode;
    //console.log(this.registerMode);
    //console.log('A7a');
  }
 // getUser(){
   // this.http.get('https://localhost:5001/Api/Users').subscribe(users => this.users =users)
 // }
  cancelRegisterMode(event:boolean)
  {
    this.registerMode=event;
  }
}
