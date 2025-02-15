import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter, withHashLocation } from '@angular/router';
import { routes } from './app/app-routing.module';
import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes, withHashLocation()), // Use HashLocationStrategy to avoid 404s on refresh
    provideHttpClient()
  ],
})
  .catch((err) => console.error(err));
