import { Component, EventEmitter, Input, Output, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NgbNav } from '@ng-bootstrap/ng-bootstrap';
import { NgbCollapse } from '@ng-bootstrap/ng-bootstrap/collapse';

interface SidebarMenuItem {
  label: string,
  path: string,
  icon: string,
  exact?: boolean
}

@Component({
  standalone: true,
	selector: 'app-sidebar-menu',
	imports: [
    RouterLink,
    RouterLinkActive
  ],
	templateUrl: './sidebar-menu.html',
  styleUrl: './sidebar-menu.scss'
})

export class SidebarMenuComponent {

  @Input() open = false;

  @Output() onChangeMenu = new EventEmitter<void>();

  selectMenuOption = () => this.onChangeMenu.emit();

  readonly menuItems: SidebarMenuItem[] = [
    {
      label: 'Dashboard',
      path: '/dashboard',
      icon: 'bi bi-graph-up-arrow',
      exact: true
    },

    {
      label: 'Hoteles',
      path: '/hotels',
      icon: 'bi bi-building'
    },

    {
      label: 'Tipos de habitación',
      path: '/room-types',
      icon: 'bi bi-house-gear'
    },

    {
      label: 'Reservaciones',
      path: '/reservations',
      icon: 'bi bi-calendar-check'
    },

    {
      label: 'Inventario',
      path: '/inventory',
      icon: 'bi bi-list-check'
    },
  ];
}