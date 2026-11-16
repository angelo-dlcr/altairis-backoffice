import { Component, DestroyRef, inject, input, output, effect, linkedSignal } from '@angular/core';
import { ModalComponent } from '../../../../shared/components/modal-component/modal-component';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { isActive, RouterLink } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { HotelApiService } from '../../services/hotel-service';
import { CreateHotelRequest, Hotel, UpdateHotelRequest } from '../../models/hotel.model';
import { ToastService } from '../../../../shared/components/toast-component/service/toast-service';
import { HttpStatusCode } from '@angular/common/http';

@Component({
  imports: [
    FormsModule,
    ReactiveFormsModule
],
  selector: 'app-hotel-form-page',
  styleUrl: './hotel-form-page.scss',
  templateUrl: './hotel-form-page.html',
})
export class HotelFormPageComponent {
  private apiService = inject(HotelApiService);
  private destroyRef = inject(DestroyRef);
  private toastService = inject(ToastService);
  
  saved = output<boolean>();
  close = output<void>();
  isFormValid = output<boolean>();

  editing = input.required<boolean>();
  viewing = input.required<boolean>();
  currentHotel = input.required<Hotel | undefined | null>()
  
  loading = output<boolean>();
  
  createRequest: CreateHotelRequest = {
    name: '',
    code: '',
    country: '',
    city: '',
    address: null
  }
  
  updateRequest: UpdateHotelRequest = {
    id: 0,
    name: '',
    code: '',
    country: '',
    city: '',
    address: null,
    isActive: true
  }

  form!: FormGroup;
  
  constructor(private fb: FormBuilder) 
  {
    this.form = this.fb.group({
      code: ['', [
        Validators.required,
        Validators.maxLength(30)
      ]],
      name: ['', [
        Validators.required,
        Validators.maxLength(150)
      ]],
      country: ['', [
        Validators.required,
        Validators.maxLength(100)
      ]],
      city: ['', [
        Validators.required,
        Validators.maxLength(100)
      ]],
      address: ['', [
        Validators.maxLength(250),
      ]],
      isActive: [true]
    });
    
    effect(() => {
      const hotel = this.currentHotel();
      const isEditing = this.editing();
      const isViewing = this.viewing();

      this.form.enable({ emitEvent: false });

      if (hotel) {
        this.form.patchValue({
          code: hotel.code,
          name: hotel.name,
          country: hotel.country,
          city: hotel.city,
          address: hotel.address,
          isActive: hotel.isActive,
        }, 
        { 
          emitEvent: false
        });
      } else {
        this.resetForm();
      }

      if (isViewing) {
        this.form.disable();
      } 
      else {
        if (isEditing) {
          this.form.get('isActive')?.enable();
        } 
        else {
          this.form.get('isActive')?.disable();
        }
      }

      this.isFormValid.emit(this.form.valid);
    });

    this.form.statusChanges
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.isFormValid.emit(this.form.valid);
      });
  }

  onSubmit(): void {
    if (this.editing()) {
      this.processUpdate();
    }
    else {
      this.processCreate();
    }
  }

  onClose(): void {
    this.form.markAsUntouched();
    this.form.clearValidators();
    this.resetForm();
    this.close.emit();
  }

  resetForm = () => this.form.reset({isActive: true}, {emitEvent: false});

  processCreate(): void {
    this.loading.emit(true);

    if (this.form.invalid) {
      this.form.markAllAsTouched();
    }
    else {
      this.createRequest.code = this.form.get('code')?.value;
      this.createRequest.name = this.form.get('name')?.value;
      this.createRequest.country = this.form.get('country')?.value;
      this.createRequest.city = this.form.get('city')?.value;
      this.createRequest.address = this.form.get('address')?.value ?? null;

      this.apiService.create(this.createRequest).subscribe({
        next: (response) => {
          if (response.success) {
            this.saved.emit(response.data!)
            
            this.toastService.show(
              'Hotel creado exitosamente.',
              'success',
            )

            this.onClose();
          }
        },
        error: (err) => {
          if(err.status === HttpStatusCode.BadRequest)
          {
            this.toastService.show(
              'No se puede registar con un código repetido',
              'warning',
            )
          }
          else {
            this.toastService.show(
              'Error en el servicio.',
              'danger',
            )
          }

          this.loading.emit(false);
        },
        complete: () => {
          this.loading.emit(false);
        }
      })
    }
  }

  processUpdate(): void {

  }
}
