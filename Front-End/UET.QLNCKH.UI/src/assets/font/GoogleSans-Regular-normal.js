﻿import { jsPDF } from "jspdf"
var callAddFont = function () {
this.addFileToVFS('GoogleSans-Regular-normal.ttf', font);
this.addFont('GoogleSans-Regular-normal.ttf', 'GoogleSans-Regular', 'normal');
};
jsPDF.API.events.push(['addFonts', callAddFont])