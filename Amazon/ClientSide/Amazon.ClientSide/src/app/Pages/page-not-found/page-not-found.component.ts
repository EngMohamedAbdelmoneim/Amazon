import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-page-not-found',
  standalone: true,
  imports: [],
  templateUrl: './page-not-found.component.html',
  styleUrl: './page-not-found.component.css'
})
export class PageNotFoundComponent implements OnInit {

  constructor(){
    this.selectedDogImage = '';
  }

  dogImages: string[] = [
    'https://images-na.ssl-images-amazon.com/images/G/01/error/37._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/75._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/187._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/113._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/150._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/40._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/50._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/105._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/80._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/71._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/34._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/96._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/114._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/5._TTD_.jpg',
    'https://images-na.ssl-images-amazon.com/images/G/01/error/121._TTD_.jpg'
  ];

  selectedDogImage: string;

  ngOnInit(): void {
    this.getRandomDogImage();
  }

  getRandomDogImage(): void {
    const randomIndex = Math.floor(Math.random() * this.dogImages.length);
    this.selectedDogImage = this.dogImages[randomIndex];
  }
}
