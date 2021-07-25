import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { Observable  } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  get(username : string, password : string) : Observable<any> {
    return this.http.get(this.apiUrl + `user/${username}/${password}`);
  }
}