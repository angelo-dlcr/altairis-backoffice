import { Routes } from '@angular/router';

import {
  MainLayoutComponent
} from './core/layout/main-layout/main-layout';

export const routes: Routes = [

  {
    path: '',
    component: MainLayoutComponent,
    children: [

      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },

      {
        path: 'dashboard',
        title: 'Dashboard',
        data: { icon: 'bi bi-graph-up-arrow' },
        loadComponent: () =>
          import('./features/dashboard/pages/dashboard-page/dashboard-page')
            .then(m => m.DashboardPageComponent)
      },

      // HOTELS
      {
        path: 'hotels',
        title: 'Hoteles',
        data: { icon: 'bi bi-building' },
        loadComponent: () =>
          import('./features/hotels/pages/hotel-list-page/hotel-list-page')
            .then(m => m.HotelListPageComponent)
      },
      {
        path: 'hotels/new',
        title: 'Hoteles',
        data: { icon: 'bi bi-building' },
        loadComponent: () =>
          import('./features/hotels/pages/hotel-form-page/hotel-form-page')
            .then(m => m.HotelFormPageComponent)
      },
      {
        path: 'hotels/:id/edit',
        title: 'Hoteles',
        data: { icon: 'bi bi-building' },
        loadComponent: () =>
          import('./features/hotels/pages/hotel-form-page/hotel-form-page')
            .then(m => m.HotelFormPageComponent)
      },
      {
        path: 'hotels/:id',
        title: 'Hoteles',
        data: { icon: 'bi bi-building' },
        loadComponent: () =>
          import('./features/hotels/pages/hotel-detail-page/hotel-detail-page')
            .then(m => m.HotelDetailPageComponent)
      },

      // ROOM TYPES
      {
        path: 'room-types',
        title: 'Tipos de habitaciones',
        data: { icon: 'bi bi-house-gear' },
        loadComponent: () =>
          import('./features/room-types/pages/room-type-list-page/room-type-list-page')
            .then(m => m.RoomTypeListPageComponent)
      },
      {
        path: 'room-types/new',
        title: 'Tipos de habitaciones',
        data: { icon: 'bi bi-house-gear' },
        loadComponent: () =>
          import('./features/room-types/pages/room-type-form-page/room-type-form-page')
            .then(m => m.RoomTypeFormPageComponent)
      },

      // INVENTORY
      {
        path: 'inventory',
        title: 'Inventario',
        data: { icon: 'bi bi-list-check' },
        loadComponent: () =>
          import('./features/inventory/pages/inventory-page/inventory-page')
            .then(m => m.InventoryPageComponent)
      },

      // RESERVATIONS
      {
        path: 'reservations',
        title: 'Reservaciones',
        data: { icon: 'bi bi-calendar-check' },
        loadComponent: () =>
          import('./features/reservations/pages/reservation-list-page/reservation-list-page')
            .then(m => m.ReservationListPageComponent)
      },
      {
        path: 'reservations/:id',
        title: 'Reservaciones',
        data: { icon: 'bi bi-calendar-check' },
        loadComponent: () =>
          import('./features/reservations/pages/reservation-detail-page/reservation-detail-page')
            .then(m => m.ReservationDetailPageComponent)
      }

    ]
  },

  {
    path: '**',
    redirectTo: 'dashboard'
  }
];