import {ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { LoaderService } from '../../../services/loader.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrl: './loader.component.css'
})
export class LoaderComponent implements OnInit  {
  isLoading: boolean = false;

  constructor(private loaderService: LoaderService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.loaderService.isLoading.subscribe((value: boolean) => {
      this.isLoading = value;
      this.cdr.detectChanges();
    })
  }
}
