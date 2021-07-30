import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GardensService } from 'src/app/services/gardens.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  userId: any;
  gardens = [];

  constructor(private router : Router, private service : GardensService) { }

  ngOnInit(): void {
    if(typeof(localStorage.userId) === 'undefined') this.router.navigateByUrl('/login')
    this.userId = localStorage.userId;
    this.service.getAll(this.userId).subscribe(data => data.gardens.forEach((element: any) => console.log(element)
    ));

    console.log(this.gardens)
  }
}
