import { Component } from '@angular/core';

@Component({
  selector: 'app-stopka',
  templateUrl: './stopka.component.html',
  styleUrls: ['./stopka.component.css']
})
export class StopkaComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
