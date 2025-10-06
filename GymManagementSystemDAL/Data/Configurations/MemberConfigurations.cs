using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemDAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystemDAL.Data.Configurations
{
    public class MemberConfigurations : GymUserConfigurations<Member>,IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
          base.Configure(builder);
            builder.Property(m => m.CreatedAt)
                .HasDefaultValueSql("GETDATE()");


        }
    }
}
