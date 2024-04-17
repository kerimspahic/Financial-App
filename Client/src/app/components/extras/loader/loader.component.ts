import { AfterViewInit, Component, OnInit } from '@angular/core';
import { LoaderService } from '../../../services/loader.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrl: './loader.component.css'
})
export class LoaderComponent implements AfterViewInit {
  isLoading: boolean = false;

  constructor(private loaderService: LoaderService) { }

  ngAfterViewInit(): void {
    this.loaderService.isLoading.subscribe((value: boolean) => {
      this.isLoading = value;
    })
  }
}
