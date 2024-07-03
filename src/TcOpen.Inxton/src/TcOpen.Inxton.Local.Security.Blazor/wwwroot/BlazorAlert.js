export function hide(id) {
  setTimeout(function () {
    $("#" + id).alert("close");
  }, 5000);
}

const alertPlaceholder = document.getElementById("alertPlaceholder");

export function alert(message, type) {
  const wrapper = document.createElement("div");

  var id = Math.floor(Math.random() * 1000000);

  switch (type) {
    case "info":
      wrapper.innerHTML = [
        '<div class="alert alert-primary alert-dismissible d-flex align-items-center fade show" role="alert" id="',
        id,
        '">',
        '<svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Info:"><use xlink:href="#info-fill" /></svg>',
        "<div>",
        message,
        "</div>",
        '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>',
        "</div>",
      ].join("");
      break;
    case "success":
      wrapper.innerHTML = [
        '<div class="alert alert-success alert-dismissible d-flex align-items-center fade show" role="alert" id="',
        id,
        '">',
        '<svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Success:"><use xlink:href="#check-circle-fill" /></svg>',
        "<div>",
        message,
        "</div>",
        '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>',
        "</div>",
      ].join("");
      break;
    case "warning":
      wrapper.innerHTML = [
        '<div class="alert alert-warning alert-dismissible d-flex align-items-center fade show" role="alert" id="',
        id,
        '">',
        '<svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Warning:"><use xlink:href="#exclamation-triangle-fill" /></svg>',
        "<div>",
        message,
        "</div>",
        '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>',
        "</div>",
      ].join("");
      break;
  }

  alertPlaceholder.append(wrapper);

  hide(id);
}
