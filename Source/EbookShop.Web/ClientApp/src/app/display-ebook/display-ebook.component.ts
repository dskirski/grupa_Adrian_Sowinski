import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ShoppingCartService } from '../shopping-cart.service';


@Component({
  selector: 'display-ebook',
  templateUrl: './display-ebook.component.html',
  styleUrls: ['./display-ebook.component.css'],
  providers: [ShoppingCartService]
})


export class DisplayEbook implements OnInit {
  title = 'Ebooki w naszej ofercie';
  ebooks = Array<Ebook>();
  p: number = 1;

  constructor(private http: HttpClient, private readonly cartService: ShoppingCartService) {
  }

  ngOnInit(): void {
    this.http.get('http://816bd8a5-da52-4f40-939c-3daa18e08845.mock.pstmn.io/sample')
      .subscribe((data: Array<Ebook>) => {
        this.ebooks = data;
      }
      );
  }

  addToCart($event, model) {
    this.cartService.addItem(model);
  }


}

interface Ebook {

  Title: string;
  Description: string;
  Price: number;
}
