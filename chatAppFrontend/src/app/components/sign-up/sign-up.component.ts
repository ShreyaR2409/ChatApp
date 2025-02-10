import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthServiceService } from '../../services/Auth/auth-service.service';
import { GoogleLoginComponent } from '../google-login/google-login.component';
declare const google: any;
@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, GoogleLoginComponent],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent implements OnInit {
  clientId: string = '586883200860-emq9ka96b7gfn0nvjg8e5q3q1vd9pulf.apps.googleusercontent.com';
  user: any = null;

  SignupForm = new FormGroup({
    name : new FormControl(''),
    email : new FormControl(''),
    password : new FormControl(''),
    retypePassword : new FormControl(''),
    profilePicture : new FormControl('')
}); 

LoginForm = new FormGroup({
  googleIdToken : new FormControl('')
}); 


  constructor(private router : Router, private authService : AuthServiceService){}
  
  ngOnInit() {
    this.loadGoogleAuth();
  }

  loadGoogleAuth() {
    const script = document.createElement('script');
    script.src = 'https://accounts.google.com/gsi/client';
    script.async = true;
    script.defer = true;
    document.body.appendChild(script);

    script.onload = () => {
      google.accounts.id.initialize({
        client_id: this.clientId,
        callback: this.handleCredentialResponse.bind(this),
      });

      google.accounts.id.renderButton(
        document.getElementById('googleSignInDiv'),
        { theme: 'outline', size: 'large' }
      );
    };
  }

  handleCredentialResponse(response: any) {
    const token = response.credential;
    console.log('token', token);
    this.LoginForm.value.googleIdToken = token;
    this.OnLogin();
  }

  OnLogin(){
    console.log(this.LoginForm.value);
    this.authService.LoginUser(this.LoginForm.value).subscribe({
      next : (data =>{
        console.log('data', data);
        this.router.navigateByUrl('dashboard');
        sessionStorage.setItem('Token' , data.accessToken);
      }),
      error : (err =>{
        console.log(err);        
      })
    })
  }
  
  Signup(){
    console.log(this.SignupForm.value);
    if(this.SignupForm.value.password == this.SignupForm.value.retypePassword){
    this.authService.CreateUser(this.SignupForm.value).subscribe({
      next: (data => {
        console.log('data', data);
        this.router.navigateByUrl('login')        
      }),
      error: (err =>{
        console.log('err', err);        
      })
    }) }
    else{
      alert("Password and retype password must be same");
    }   
  }
}
