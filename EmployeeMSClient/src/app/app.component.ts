// src/app/app.component.ts
import { Component, Inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router'; // Import RouterModule for routing
import { SharedModule } from './core/_shared/shared.module';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-root',
  imports: [RouterModule, CommonModule, SharedModule], // Ensure RouterModule is imported
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Employee Management System';
}
