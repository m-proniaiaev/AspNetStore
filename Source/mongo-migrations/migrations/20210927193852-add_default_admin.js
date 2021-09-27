module.exports = {
  async up(db, client) {
    var roleId = '113e2370-8d7b-4937-ac4f-f8213e5094f9';
    var admin = {
      _id: '8e04e99e-ffea-4255-9843-753b12cd06ca',
      Name: 'DefaultAdmin',
      IsActive: true,
      Role: roleId,
      Hash: '10000.2kVMXEAPabSTUmz2o6B8fwIUKtRUDpZQxkUEJnfzas0=',
      Salt: 'sGI9M6I0QQvAh2CbTVewgA==',
      Created: '2021-09-27T19:43:40.610Z',
      CreatedBy: '00000000-0000-0000-0000-000000000000',
      Edited: null,
      EditedBy: null
    };
    var roleJson = `      {
      "_id": "113e2370-8d7b-4937-ac4f-f8213e5094f9",
      "Name": "Admin",
      "RoleType": 1,
      "IsActive": true,
      "Actions": [ 
          "User-Get", 
          "Users-Get", 
          "User-Create", 
          "User-Update", 
          "User-UpdateRole", 
          "User-UpdatePassword", 
          "User-Disable", 
          "User-Delete", 
          "Role-Get", 
          "Roles-Get", 
          "Actions-Get", 
          "Role-Create", 
          "Role-Update", 
          "Role-Disable", 
          "Role-Delete", 
          "Seller-Get", 
          "Sellers-Get", 
          "Seller-Create", 
          "Seller-Update", 
          "Seller-Delete", 
          "Record-Get", 
          "Records-Get", 
          "Record-Create", 
          "Record-Sell", 
          "Record-Delete", 
          "Record-Update", 
          "LogOut"
      ],
      "Created": "2021-09-27T19:43:09.363Z",
      "CreatedBy": "00000000-0000-0000-0000-000000000000",
      "Edited": null,
      "EditedBy": null
      }`;

    var role = JSON.parse(roleJson);
    await db.collection('Roles').insertOne(role);
    await db.collection('Users').insertOne(admin);
  },

  async down(db, client) {
      //NO rollback here :)
  }
};
