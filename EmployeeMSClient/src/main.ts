import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter, withHashLocation } from '@angular/router';
import { routes } from './app/app-routing.module';
import { provideHttpClient } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { importProvidersFrom } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes, withHashLocation()), // Use HashLocationStrategy to avoid 404s on refresh
    provideHttpClient(),
    importProvidersFrom(
      ToastrModule.forRoot({
        timeOut: 3000, // Set your preferred config options here
        positionClass: 'toast-bottom-right',
        preventDuplicates: true,
      })
    ),
    importProvidersFrom(BrowserAnimationsModule), // Add BrowserAnimationsModule here
  ],
}).catch((err) => console.error(err));
