import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface ParkingLot {
  name: string;
  availableSpots: { [key: string]: number };
  feeModel: { [key: string]: number };
}

interface Vehicle {
  type: string;
  durationInMinutes: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  parkingLots: ParkingLot[] = [];
  vehicleType = 'Motorcycles/scooters';
  duration = 0;
  fee: number | null = null;

  constructor(private http: HttpClient) {
    this.fetchParkingLots();
  }

  fetchParkingLots() {
    this.http.get<ParkingLot[]>('https://localhost:7037/api/parking/parking-lots')
      .subscribe(parkingLots => this.parkingLots = parkingLots);
  }

  calculateFee() {
    const vehicle: Vehicle = {
      type: this.vehicleType,
      durationInMinutes: this.duration
    };

    this.http.post<number>('http://localhost:7307/api/parking/calculate-fee', vehicle)
      .subscribe(fee => this.fee = fee);
  }
}
