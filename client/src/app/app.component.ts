import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Dating App';
  users: any; //for now
  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.setCurrentUser()
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user')!); //https://stackoverflow.com/questions/46915002/argument-of-type-string-null-is-not-assignable-to-parameter-of-type-string
    this.accountService.setCurrentUser(user);
  }
}
