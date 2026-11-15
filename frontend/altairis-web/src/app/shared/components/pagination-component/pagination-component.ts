import { Component, computed, input, output } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-pagination-component',
  styleUrl: './pagination-component.scss',
  templateUrl: './pagination-component.html',
})

export class PaginationComponent {
  totalCount = input<number>(0);
  pageNumber = input<number>(1);
  pageSize = input<number>(10);
  totalPages = input<number>(0);

  pageChange = output<number>();

  pagesArray = computed(() =>
    Array.from({ length: this.totalPages() }, (_, index) => index + 1)
  );

  goToPage(page: number): void {
    if (
      page < 1 ||
      page > this.totalPages() ||
      page === this.pageNumber()
    ) {
      return;
    }

    this.pageChange.emit(page);
  }

  previousPage(): void {
    this.goToPage(this.pageNumber() - 1);
  }

  nextPage(): void {
    this.goToPage(this.pageNumber() + 1);
  }

  firstPage(): void {
    this.goToPage(1);
  }

  lastPage(): void {
    this.goToPage(this.totalPages());
  }
}