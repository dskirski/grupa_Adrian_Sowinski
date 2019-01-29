import { Injectable } from '@angular/core';

@Injectable()
export class ShoppingCartService {

  static readonly CartStorageKey = "shopping-cart";

  addItem(ebookModel: any) {
    for (const element of this.cart) {
      if (element.Title === ebookModel.Title) {
        alert("Ten przedmiot już jest w koszyku");
        return;
      }
    }

    var cart = this.cart;
    cart.push(ebookModel);
    this.cart = cart;
  }

  removeItem(ebookModel: any) {
    let idx = 0;
    for (const element of this.cart) {
      if (element.Title === ebookModel.Title) {
        var cart = this.cart;
        cart.splice(idx, 1);
        this.cart = cart;
        return;
      }

      idx++;
    }
  }

  count() {
    return this.cart.length;
  }

  total() {
    return this.cart.reduce((acc, item) => acc += item.Price, 0);
  }

  getCart() {
    return this.cart;
  }

  private hasItem(ebookModel: any) {
    return this.cart.filter(x => x.Title === ebookModel.Title).length > 0;
  }

  private get cart(): any[] {
    var serializedCart = localStorage.getItem(ShoppingCartService.CartStorageKey);

    if (serializedCart == null)
      return [];

    return JSON.parse(serializedCart);
  }

  private set cart(value) {
    localStorage.setItem(ShoppingCartService.CartStorageKey, JSON.stringify(value));
  }

}
