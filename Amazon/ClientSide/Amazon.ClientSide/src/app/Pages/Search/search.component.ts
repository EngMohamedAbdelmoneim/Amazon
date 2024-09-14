import { Component } from '@angular/core';
import { log } from 'node:console';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {

  constructor()
  {
    console.table(this.products);
  }

  ngOnInit()
  {
    fetch("https://localhost:7283/Product").then((res) => {
      return res.json();
    })
    .then((data) => console.log(data))
    .catch((err) => console.log(err));
  }

  products = [
    {
      id: 1,
      name: 'Laptop',
      price: 999.99,
      category: 'Electronics'
    },
    {
      id: 2,
      name: 'Smartphone',
      price: 799.99,
      category: 'Electronics'
    },
    {
      id: 3,
      name: 'Headphones',
      price: 199.99,
      category: 'Electronics'
    },
    {
      id: 4,
      name: 'Coffee Maker',
      price: 49.99,
      category: 'Home Appliances'
    },
    {
      id: 5,
      name: 'Blender',
      price: 39.99,
      category: 'Home Appliances'
    },
    {
      id: 6,
      name: 'Book',
      price: 14.99,
      category: 'Books'
    }
  ];



  LowtoHighSort()
  {
    let p = this.products.sort((a, b) => a.price - b.price);
    console.log(p);
  }

  HightoLowSort()
  {
    let p = this.products.sort((a, b) => b.price - a.price);
    console.log(p); 
  }

  filterProducts()
  {
    let minValue = (<HTMLInputElement>document.getElementById("minValue")).value;
    let maxValue = (<HTMLInputElement>document.getElementById("maxValue")).value;

    console.log(minValue);
    console.log(maxValue);
    
    let p = this.products.filter(p => p.price > parseInt(minValue) && p.price < parseInt(maxValue));
    console.table(p);
  }

}
