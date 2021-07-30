import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/services/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = "";
  password: string = "";

  constructor(private router : Router, private service : UsersService) {}

  ngOnInit(): void {
    if(typeof(localStorage.userId) !== 'undefined') localStorage.removeItem('userId')
  }

  login() {
    this.service.get(this.username, this.password).subscribe(data => {
      if(data.status !== 0 || typeof(data.status) === 'undefined') return;
      localStorage.setItem('userId', data.user.id.toString());
      this.router.navigateByUrl('/');
    });
  }
}
