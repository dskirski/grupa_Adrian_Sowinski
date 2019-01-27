import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-nav-menu1',
  templateUrl: './nav-menu1.component.html',
  styleUrls: ['./nav-menu1.component.css'],
  providers: [UserService]
})
export class NavMenu1Component implements OnInit{
  status: boolean;
  constructor(private userService: UserService) {
   }

  isLoggedIn() {
    return this.userService.isLoggedIn();
}

  ngOnInit() {
    this.status = this.userService.isLoggedIn();
  }
  logout() {
    this.userService.logout();
    this.status = false;
  }
  isExpanded = false;
  
  collapse() {
    this.isExpanded = false;
  }

  logOut() {
    this.userService.logout();
    alert("Wylogowano pomyślnie.");
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
