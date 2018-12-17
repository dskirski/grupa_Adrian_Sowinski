import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu1',
  templateUrl: './nav-menu1.component.html',
  styleUrls: ['./nav-menu1.component.css']
})
export class NavMenu1Component {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
