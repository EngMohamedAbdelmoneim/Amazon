export class Address 
{
    constructor(id: string, buildingName: string, city: string, governorate: string, country: string, district: string, firstName: string, lastName: string, nearestLandMark: string, phoneNumber: string, streetAddress: string)
    {
        this.id = id;
        this.buildingName = buildingName;  
        this.city = city; 
        this.country = country; 
        this.district = district
        this.firstName = firstName
        this.governorate = governorate;
        this.lastName = lastName;
        this.nearestLandMark = nearestLandMark;
        this.phoneNumber = phoneNumber;
        this.streetAddress = streetAddress;

    }

    id: string;
    buildingName: string;
    city: string;
    country: string;
    district: string;
    firstName: string;
    governorate: string
    lastName: string;
    nearestLandMark: string;
    phoneNumber: string;
    streetAddress: string;
}