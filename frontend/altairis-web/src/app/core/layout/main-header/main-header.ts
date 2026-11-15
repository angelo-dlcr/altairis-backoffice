import { Component, inject, Input } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { NavigationEnd, Router } from '@angular/router';
import { filter, map } from 'rxjs';

@Component({
  imports: [],
  selector: 'app-main-header',
  styleUrl: './main-header.scss',
  templateUrl: './main-header.html',
})

export class MainHeaderComponent {

  private router = inject(Router);

  private getCurrentRoute() {
      let route = this.router.routerState.root;

      while (route.firstChild) {
        route = route.firstChild;
      }

      return route.snapshot;
  }

  private navigationEnd$ = this.router.events.pipe(
    filter(event => event instanceof NavigationEnd)
  )

  public curerntSectionName = toSignal(
    this.navigationEnd$.pipe(
      map(() => this.getCurrentRoute().title || 'Dashboard')),
      {initialValue: 'Dashboard'}
  )

  public curerntSectionIcon = toSignal(
    this.navigationEnd$.pipe(
      map(() => this.getCurrentRoute().data['icon'] || 'bi bi-graph-up-arrow')),
      {initialValue: 'bi bi-graph-up-arrow'}
  )
}
