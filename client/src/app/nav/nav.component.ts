import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};
  //currentUser$: Observable<User | null> = of(null); //using rxjs

  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService,
    private title: Title
  ) {}

  ngOnInit(): void {
    this.title.setTitle('Home | Dating App'); //set document title
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/members');
        console.log(response);
      },
      error: (error) => {
        //  this.toastr.error('Invalid Username of Password!', '');
        console.log(error);
      },
    });
    console.log(this.model);
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
