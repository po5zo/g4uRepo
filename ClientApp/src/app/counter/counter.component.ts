import { MatButtonModule } from '@angular/material';
import { Component } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;
  public currentCount2 = 0;

  incrementCounter() {
    let szam:number = 10;
    this.currentCount = szam;
  }
  changeCurrent() {
    this.currentCount2 = 60;
  }
}
