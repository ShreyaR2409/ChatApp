import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
@Injectable({
  providedIn: 'root'
})
export class JwtService {

  constructor() { }

  getEmail(){
    const token = sessionStorage.getItem("Token");
    // console.log('Token', token);
    
    if(token){
      const decodedToken : any = jwtDecode(token);
      console.log('decodedToken', decodedToken);      
      return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
    }
  }

  getName(){
    const token = sessionStorage.getItem("Token");
    if(token){
      const decodedToken : any = jwtDecode(token);
      return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    }
  }

}
