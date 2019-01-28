import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from '../shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css'],
  providers: [ShoppingCartService]
})
export class ShoppingCartComponent implements OnInit {

  constructor(private readonly cartService: ShoppingCartService) { }

  ngOnInit() {
  }

  removeFromCart($event, model) {
    this.cartService.removeItem(model);
  }

  purchase($event, model) {
    alert("Coming soon!");
  }
}
