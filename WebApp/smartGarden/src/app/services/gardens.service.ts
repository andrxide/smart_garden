import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable  } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GardensService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAll(gardenId : string) : Observable<any> {
    return this.http.get(this.apiUrl + `garden/${gardenId}`);
  }
}
