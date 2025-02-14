import { Component } from '@angular/core';
import { JwtService } from '../../services/Jwt/jwt.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
declare var google: any;
@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  users = [
    { name: 'Alice', lastMessage: 'Hey!', image: 'https://via.placeholder.com/40' },
    { name: 'Bob', lastMessage: 'See you soon', image: 'https://via.placeholder.com/40' }
  ];

  messages = [
    { text: 'Hello!', sent: true },
    { text: 'Hi there!', sent: false },
    { text: 'How are you?', sent: true }
  ];
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
