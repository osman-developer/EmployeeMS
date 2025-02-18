// src/app/app.component.ts
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router'; // Import RouterModule for routing
import { SharedModule } from './core/_shared/shared.module';

@Component({
  selector: 'app-root',
  imports: [RouterModule, SharedModule], // Ensure RouterModule is imported
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'EmployeeMSClient';
}
