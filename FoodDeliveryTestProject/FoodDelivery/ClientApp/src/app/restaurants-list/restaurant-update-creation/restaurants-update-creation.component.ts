import { ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from '../../alert-system/alert.service';
import { RestaurantImage } from '../../models/restaurant-image.model';
import { Restaurant } from '../../models/restaurant.model';
import { AuthenticationService } from '../../services/authentication.service';
import { RestaurantsService } from '../../services/restaurants.service';

@Component({
  selector: 'app-restaurants-update-creation',
  templateUrl: './restaurants-update-creation.component.html'
})
export class RestaurantUpdateCreationComponent implements OnInit {

  get r() { return this.restaurantForm.controls; }

  restaurantForm = this.formBuilder.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    image: ['', Validators.required]
  });

  options = {
    autoClose: true,
    keepAfterRouteChange: false
  };

  public restaurant: Restaurant;
  public submitted = false;
  private imageStr = 'data:image/jpeg;base64,';

  constructor(private route: ActivatedRoute,
    private router: Router,
    private restaurantService: RestaurantsService,
    private formBuilder: FormBuilder,
    private cd: ChangeDetectorRef,
    private authenticationService: AuthenticationService,
    private alertService: AlertService) {
  }
  async ngOnInit(): Promise<void> {
    this.route.params.subscribe(async (params) => {
      if (params) {
        const restaurantId = params['id'];
        if (restaurantId) {
          this.restaurant = await this.restaurantService.getRestaurant(restaurantId, this.authenticationService.getUserId);
          this.r.name.setValue(this.restaurant.name);
          this.r.description.setValue(this.restaurant.description);
          var image = await this.restaurantService.getImage(restaurantId);
          this.r.image.setValue(this.formatImage(image));
          return;
        }
        else {
          this.restaurant = <Restaurant>{};
        }
      }
    });
  }
  onFileChange(event) {
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        if (!reader.result.toString().startsWith(this.imageStr)) {
          this.alertService.warn("Please use jpeg file", this.options);
          return;
        }
        this.restaurantForm.patchValue({
          image: reader.result
        });
        this.cd.markForCheck();
      };
    }
  }

  async onSubmit(): Promise<void> {
    this.submitted = true;
    if (this.restaurantForm.invalid) {
      return;
    }
    this.restaurant.description = this.r.description.value;
    this.restaurant.name = this.r.name.value;
    this.restaurant.image = <RestaurantImage>{};
    this.restaurant.image.image = this.formatArrayFromImgag(this.r.image.value);
    if (!this.restaurant.entityId) {
      this.restaurant.ownerId = this.authenticationService.getUserId;
      await this.restaurantService.addRestaurant(this.restaurant, this.authenticationService.getUserId);
      this.alertService.success("Restaurant added", this.options)
    }
    else {
      await this.restaurantService.updateRestaurant(this.restaurant, this.authenticationService.getUserId);
    }
    this.router.navigate(['']);

  }

  formatImage(img: any): any {
    return 'data:image/jpeg;base64,' + img;
  }

  formatArrayFromImgag(img: string): any {
    return img.replace(this.imageStr, '');
  }
}
