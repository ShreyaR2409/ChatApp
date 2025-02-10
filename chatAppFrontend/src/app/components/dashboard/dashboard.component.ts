import { Component } from '@angular/core';
import { JwtService } from '../../services/Jwt/jwt.service';
import { Router } from '@angular/router';
declare var google: any;
@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  Email : string;
  constructor(private jwt : JwtService, private router: Router){
    this.Email = this.jwt.getEmail();
    console.log('Email', this.Email)
  }

  logout(){
    console.log("Logout called");    
    google.accounts.id.disableAutoSelect(); 
    sessionStorage.removeItem('Token');
    this.router.navigateByUrl('login');
  }
}
