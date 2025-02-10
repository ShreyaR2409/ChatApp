import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  constructor(private http : HttpClient) { }
  private url = 'https://localhost:7261/api/User';

  public LoginUser(LoginData : any): Observable<any>{
    return this.http.post<any>(`${this.url}/Login`, LoginData)
  }

  public CreateUser(UserData : any): Observable<any>{
    return this.http.post<any>(`${this.url}/create`,UserData);
  }

}
