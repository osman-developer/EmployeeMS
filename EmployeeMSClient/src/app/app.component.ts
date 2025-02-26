// src/app/app.component.ts
import { Component, Inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router'; // Import RouterModule for routing
import { SharedModule } from './core/_shared/shared.module';
import { CommonModule } from '@angular/common';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-root',
  imports: [RouterModule, CommonModule, NgbModalModule, SharedModule], // Ensure RouterModule is imported
  providers:[NgbModalModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Employee Management System';
}
