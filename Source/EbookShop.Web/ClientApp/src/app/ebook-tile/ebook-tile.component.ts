import { Component, OnInit, Input } from '@angular/core';
import { ShoppingCartService } from '../shopping-cart.service';

@Component({
  selector: 'ebook-tile',
  templateUrl: './ebook-tile.component.html',
  styleUrls: ['./ebook-tile.component.css'],
  providers: [ShoppingCartService]
})
export class EbookTileComponent implements OnInit {

  @Input() ebookModel: any;
  @Input() buttonCallback: Function;
  @Input() buttonText: string;

  constructor(private readonly cartService: ShoppingCartService) { }

  ngOnInit() {
  }

  onClick($event, model) {
    this.buttonCallback($event, model);
  }

}
