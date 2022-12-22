import { Component, OnInit } from '@angular/core';
import { Router, TitleStrategy } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';
import { UserParams } from '../_models/userParams';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};

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
        this.model = {};
        window.location.reload();
        //console.log(response);
      },
      error: (error) => console.log(error),
      // complete: () => window.location.reload(),
    });
    console.log(this.model);
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
