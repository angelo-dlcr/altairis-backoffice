import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarMenuComponent } from './core/layout/sidebar-menu/sidebar-menu';
import { MainHeaderComponent } from './core/layout/main-header/main-header';
import { MainLayoutComponent } from './core/layout/main-layout/main-layout';

@Component({
  imports: [
    RouterOutlet,
    SidebarMenuComponent,
    MainHeaderComponent,
    MainLayoutComponent
],
  selector: 'app-root',
  styleUrl: './app.scss',
  templateUrl: './app.html',
})

export class App {
  protected readonly title = signal('altairis-web');

  sectionName: string = 'Dashboard';
  sectionIcon: string = 'bi bi-graph-up-arrow';

  updateSection(section: string, icon: string) {
    this.sectionName = section;
    this.sectionIcon = icon;
  }
}
