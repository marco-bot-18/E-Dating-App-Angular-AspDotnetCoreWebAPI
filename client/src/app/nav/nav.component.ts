import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};
  //currentUser$: Observable<User | null> = of(null); //using rxjs

  constructor(public accountService: AccountService) {}

  ngOnInit(): void {
    //this.currentUser$ = this.accountService.currentuser$;
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
    });
    console.log(this.model);
  }

  logout() {
    this.accountService.logout();
  }
}
