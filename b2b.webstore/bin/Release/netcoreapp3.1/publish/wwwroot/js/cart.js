class CartRemoveButton extends HTMLElement {
  constructor() {
    super();
    this.addEventListener('click', (event) => {
      event.preventDefault();
      this.closest('cart-template').updateQuantity(this.dataset.index, 0);
    });
  }
}

customElements.define('cart-remove-button', CartRemoveButton);

class CartItems extends HTMLElement {
  constructor() {
    super();

    this.lineItemStatusElement = document.getElementById('shopping-cart-line-item-status');

    this.currentItemCount = Array.from(this.querySelectorAll('[name="updates[]"]'))
      .reduce((total, quantityInput) => total + parseInt(quantityInput.value), 0);

    this.debouncedOnChange = debounce((event) => {
      this.onChange(event);
    }, 300);

    this.addEventListener('change', this.debouncedOnChange.bind(this));
  }

    onChange(event) {
        this.updateQuantity(event.target.dataset.index, event.target.value, document.activeElement.getAttribute('name'), this.getSectionsToRender);
  }

  getSectionsToRender() {
    return [
      {
        id: 'main-cart-items',
        section: document.getElementById('main-cart-items').dataset.id,
        selector: '.js-contents',
      },
      {
        id: 'cart-products-count',
        section: 'cart-products-count',
        selector: '.shopify-section'
      },
      {
        id: 'cart-live-region-text',
        section: 'cart-live-region-text',
        selector: '.shopify-section'
      },
      {
        id: 'cart-footer',
        section: document.getElementById('cart-footer').dataset.id,
        selector: '.js-contents',
      }
    ];
  }

  updateQuantity(line, quantity, name,func1) {
     /* this.enableLoading(line);*/

    const body = JSON.stringify({
      line,
      quantity,
      sections: this.getSectionsToRender().map((section) => section.section),
      sections_url: window.location.pathname
    });
      $.ajax({
          type: "post",
          async:false,
          url: "Cart/Index/?handler=Change",
          data: "quantity="+quantity+"&line=" + line,
          contentType: 'application/x-www-form-urlencoded',
          dataType: "json",
          headers:
          {
              "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
          },
          dataType: "json",

          beforeSend: function () {
              
          },
          success: function (n) {
              const parsedState = n;
              document.getElementsByClassName('main-cart')[0].firstElementChild.classList.toggle('hide', parsedState.item_count === 0);
              const cartEmpty = document.getElementById('cartEmpty');
              if (parsedState.item_count === 0) {
                  cartEmpty.classList.remove('hide', parsedState.item_count === 0);
              }
              const cartFooter = document.getElementById('cart-footer');

              if (cartFooter) cartFooter.classList.toggle('hide', parsedState.item_count === 0);
              const lineItem = document.getElementById(`CartItem-${line}`);
              if (quantity == 0) {
                  lineItem.remove();
                  document.getElementById("cart_total").innerHTML = parsedState.total_price + " RSD.";
              }
              else {
                  /*       document.getElementById("cart-item-final-price-" + line).innerHTML = parsedState.current_total_price.toFixed(2) + " RSD.";*/
                  var item_final_price = parseFloat(parsedState.current_total_price);
                  var total_price = parseFloat(parsedState.total_price);
                  var parts = item_final_price.toFixed(2).split(".");
                  var num = parts[0].replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,") +
                      (parts[1] ? "." + parts[1] : "");
                  document.getElementById("cart-item-final-price-" + line).innerHTML = num + " RSD.";
                  var parts = total_price.toFixed(2).split(".");
                  var num = parts[0].replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,") +
                      (parts[1] ? "." + parts[1] : "");
                  document.getElementById("cart_total").innerHTML = num + " RSD.";
              }
              //this.updateLiveRegions(line, parsedState.item_count);
             
              if (lineItem && lineItem.querySelector(`[name="${name}"]`)) lineItem.querySelector(`[name="${name}"]`).focus();
              document.getElementById("main-cart-items").classList.remove("cart__items--disabled");
            
              /*this.disableLoading();*/
              //console.log(n);
              //t.hideLoading();
              //t.showModalCart(".ajax-success-modal");
              //e('.ajax-success-modal.cart-popup-wrapper').addClass('open');
              //e(".ajax-success-modal").find(".ajax-product-image").attr("src", a);
              //e(".ajax-success-modal").find(".added-to-wishlist").hide();
              //e(".ajax-success-modal").find(".added-to-cart").show();
              //e(".ajax-success-modal").find(".ajax-product-title").text(p);
              //e(".ajax-success-modal").find(".ajax_price").html(amt);
              //e(".ajax-success-modal").find(".ajax_price").html(amt1);
              //e(".ajax-success-modal .total_itmes .popup-qty").text(r);
              //t.updateDropdownCart()
          },
          error: function (n, r) {
              //t.hideLoading();
              //e(".ajax-error-message").text(e.parseJSON(n.responseText).description);
              //t.showModalCart(".ajax-error-modal")
          }
      })

    //fetch(`${routes.cart_change_url}`, {...fetchConfig(), ...{ body }})
      //fetch(`${routes.cart_change_url}`, {
      //    method: "POST",
      //    mode: "cors", // no-cors, *cors, same-origin
      //    cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
      //    credentials: "same-origin", // include, *same-origin, omit
      //    headers: {
      //        "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val(),
      //        'Content-Type': 'application/x-www-form-urlencoded',
      //    },
      //    redirect: "follow", // manual, *follow, error
      //    referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
      //    body: "line=" + line + "&quantity=" + quantity, // body data type must match "Content-Type" header
      //})
      //.then((response) => {
      //  return response.text();
      //})
      //.then((state) => {
      //  const parsedState = JSON.parse(state);
      //  this.firstElementChild.classList.toggle('hide', parsedState.item_count === 0);
      //const cartEmpty = document.getElementById('cartEmpty');
      //if (parsedState.item_count === 0) {
      //cartEmpty.classList.remove('hide', parsedState.item_count === 0);
      //}
      //  const cartFooter = document.getElementById('cart-footer');

      //  if (cartFooter) cartFooter.classList.toggle('hide', parsedState.item_count === 0);

      //  //this.getSectionsToRender().forEach((section => {
      //  //  const elementToReplace =
      //  //    document.getElementById(section.id).querySelector(section.selector) || document.getElementById(section.id);
      //  //  elementToReplace.innerHTML =
      //  //    this.getSectionInnerHTML(parsedState.sections[section.section], section.selector);
      //  //}));

      //  //this.updateLiveRegions(line, parsedState.item_count);
      //  const lineItem =  document.getElementById(`CartItem-${line}`);
      //  if (lineItem && lineItem.querySelector(`[name="${name}"]`)) lineItem.querySelector(`[name="${name}"]`).focus();
      //  this.disableLoading();
      //}).catch(() => {
      //  this.querySelectorAll('.loading-overlay').forEach((overlay) => overlay.classList.add('hidden'));
      //  document.getElementById('cart-errors').textContent = window.cartStrings.error;
      //  this.disableLoading();
      //});
  }

  updateLiveRegions(line, itemCount) {
    if (this.currentItemCount === itemCount) {
      document.getElementById(`Line-item-error-${line}`)
        .querySelector('.cart-item__error-text')
        .innerHTML = window.cartStrings.quantityError.replace(
          '[quantity]',
          document.getElementById(`Quantity-${line}`).value
        );
    }

    this.currentItemCount = itemCount;
    this.lineItemStatusElement.setAttribute('aria-hidden', true);

    const cartStatus = document.getElementById('cart-live-region-text');
    cartStatus.setAttribute('aria-hidden', false);

    setTimeout(() => {
      cartStatus.setAttribute('aria-hidden', true);
    }, 1000);
  }

  getSectionInnerHTML(html, selector) {
    return new DOMParser()
      .parseFromString(html, 'text/html')
      .querySelector(selector).innerHTML;
  }

  enableLoading(line) {
    document.getElementById('main-cart-items').classList.add('cart__items--disabled');
    this.querySelectorAll(`#CartItem-${line} .loading-overlay`).forEach((overlay) => overlay.classList.remove('hidden'));
    document.activeElement.blur();
    this.lineItemStatusElement.setAttribute('aria-hidden', false);
  }

  disableLoading() {
    document.getElementById('main-cart-items').classList.remove('cart__items--disabled');
  }
}

customElements.define('cart-template', CartItems);
