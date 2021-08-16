module.exports = {
  async up(db, client) {
    await db.collection('Users').updateMany({}, {$set: {CreatedBy: "81a058a5-99cc-4576-8963-86be537179e8"}});
    await db.collection('Roles').updateMany({}, {$set: {CreatedBy: "81a058a5-99cc-4576-8963-86be537179e8"}});
    await db.collection('Sellers').updateMany({}, {$set: {CreatedBy: "81a058a5-99cc-4576-8963-86be537179e8"}});
    await db.collection('Records').updateMany({}, {$set: {CreatedBy: "81a058a5-99cc-4576-8963-86be537179e8"}});
  },

  async down(db, client) {
    await db.collection('Users').updateMany({}, {$set: {CreatedBy: "00000000-0000-0000-0000-000000000000"}});
    await db.collection('Roles').updateMany({}, {$set: {CreatedBy: "00000000-0000-0000-0000-000000000000"}});
    await db.collection('Sellers').updateMany({}, {$set: {CreatedBy: "00000000-0000-0000-0000-000000000000"}});
    await db.collection('Records').updateMany({}, {$set: {CreatedBy: "00000000-0000-0000-0000-000000000000"}});
  }
};
