import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css'],
})
export class TestErrorComponent implements OnInit {
  baseUrl: string = environment.apiUrl;
  validationErrors: string[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  get404Error() {
    this.http.get(this.baseUrl + 'bug/not-found').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
      complete: () => console.log('Not Found Error Completed!'),
    });
  }

  get400Error() {
    this.http.get(this.baseUrl + 'bug/bad-request').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
      complete: () => console.log('Bad Request Completed!'),
    });
  }

  get500Error() {
    this.http.get(this.baseUrl + 'bug/server-error').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
      complete: () => console.log('Server Error Completed!'),
    });
  }

  get401Error() {
    this.http.get(this.baseUrl + 'bug/auth').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
      complete: () => console.log('Auth Error!'),
    });
  }

  get400ValidationError() {
    this.http.post(this.baseUrl + 'account/register', {}).subscribe({
      next: (response) => console.log(response),
      error: (error) => {
        console.log(error);
        this.validationErrors = error;
      },
      complete: () => console.log('Validation Error!'),
    });
  }
}
