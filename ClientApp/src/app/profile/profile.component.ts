import { AuthService } from './../../auth/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  profile: any;

  constructor(public auth: AuthService) { }

  ngOnInit() {
    if (this.auth.userProfile && this.auth.isAuthenticated) {
      this.profile = this.auth.userProfile;
    }
    else if (this.auth.isAuthenticated) {
      this.auth.getProfile((err, profile) => {
        this.profile = profile;
      });
    }
    else alert("You must log in before.")
  }
}